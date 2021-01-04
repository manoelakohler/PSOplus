using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using PSO.Optimization.Model;
using Application = Microsoft.Office.Interop.Excel.Application;

namespace PSO.Optimization.Utils
{
    public static class Services
    {
        public static bool OnlyOneIterationOptimization { get; set; }
        public static Worksheet XlWorkSheet { get; set; }
        public static Workbook XlWorkBook { get; set; }
        public static Application XlApp { get; set; }
        private static int InitialCell { get; set; }
        private static int FinalCell { get; set; }
        private static List<Tuple<double, double, double, double, double, double>> BestAverageAndWorstFromIterations { get; set; }
        private static int _lineIndex = 1;
        private static int LineIndex
        {
            get { return _lineIndex; }
            set { _lineIndex = value; }
        }
        private static double _bestTimeCost = Double.MaxValue;
        private static double BestTimeCost
        {
            get { return _bestTimeCost; }
            set { _bestTimeCost = value; }
        }


        /// <summary>
        /// Cria o arquivo excel com os valores de evolução e plota o gráfico da evolução no excel
        /// </summary>
        /// <param name="bestMeanWorstSdList"></param>
        /// <param name="excelPath"></param>
        /// <param name="iteration"></param>
        /// <param name="timeCost"></param>
        /// <param name="targetPartile"></param>
        /// <param name="benchmarkBestCost"></param>
        /// <param name="bestSolutionFromAll"></param>
        /// <param name="bestSolutionFromCurrentIteration"></param>
        /// <param name="worstCost"></param>
        /// <param name="std"></param>
        /// <param name="bestCostFromAll"></param>
        public static void CreateExcelFile(List<Tuple<double, double, double, double, double, double, Tuple<double, double, double>>> bestMeanWorstSdList,
            string excelPath, int iteration, double timeCost, string[] targetPartile, double benchmarkBestCost,
            double[] bestSolutionFromAll, double[] bestSolutionFromCurrentIteration, double worstCost, List<double> std, double bestCostFromAll)
        {
            object misValue = System.Reflection.Missing.Value;

            Initialize(excelPath, iteration, misValue);

            WriteResultsByGeneration(bestMeanWorstSdList, bestSolutionFromCurrentIteration);
            FinalCell = LineIndex - 1;
            LineIndex++;

            CreateChart(XlWorkSheet, InitialCell, FinalCell, iteration);
            CreateReferenceChart(XlWorkSheet, InitialCell, FinalCell, iteration);

            BestAverageAndWorstFromIterations.Add(Tuple.Create(bestMeanWorstSdList.Last().Item1,
                bestMeanWorstSdList.Last().Item2, bestMeanWorstSdList.Last().Item3, bestMeanWorstSdList.Last().Item5,
                bestMeanWorstSdList.Last().Item6, bestMeanWorstSdList.Last().Item7.Item1));

            //Atualiza melhor tempo
            if (timeCost < BestTimeCost)
                BestTimeCost = timeCost;

            Tuple<double, double, double, double, double, double, double> calculations;
            if (OnlyOneIterationOptimization)
            {
                calculations = CalculateBestAverageAndWorstFromIterations(benchmarkBestCost, bestCostFromAll);
                Finalize(excelPath, benchmarkBestCost, bestSolutionFromAll, std, calculations, misValue, targetPartile, worstCost);
                return;
            }
            //Calcula estatísticas finais e libvera excel se essa é a última iteração
            if (iteration != PsoConfiguration.NumberOfIterations ) return;
            calculations = CalculateBestAverageAndWorstFromIterations(benchmarkBestCost, bestCostFromAll);
            Finalize(excelPath, benchmarkBestCost, bestSolutionFromAll, std, calculations, misValue, targetPartile, worstCost);
        }

        /// <summary>
        /// Escreve no excel o melhor, pior e a média de cada geração.
        /// </summary>
        /// <param name="bestMeanWorstList"></param>
        /// <param name="bestSolutionFromCurrentIteration"></param>
        private static void WriteResultsByGeneration(
            List<Tuple<double, double, double, double, double, double, Tuple<double, double, double>>> bestMeanWorstList,
            double[] bestSolutionFromCurrentIteration)
        {
            var generation = 1;
            var topLineFromIteration = LineIndex;
            foreach (var tuple in bestMeanWorstList)
            {
                //i+2 : in Excel file row index is starting from 1. It's not a 0 index based collection
                XlWorkSheet.Cells[LineIndex, 1] = generation;
                XlWorkSheet.Cells[LineIndex, 2] = tuple.Item1;
                XlWorkSheet.Cells[LineIndex, 3] = tuple.Item2;
                XlWorkSheet.Cells[LineIndex, 4] = tuple.Item3;
                XlWorkSheet.Cells[LineIndex, 5] = tuple.Item4;
                XlWorkSheet.Cells[LineIndex, 6] = tuple.Item5;
                XlWorkSheet.Cells[LineIndex, 7] = tuple.Item6;
                XlWorkSheet.Cells[LineIndex, 8] = tuple.Item7.Item1;
                XlWorkSheet.Cells[LineIndex, 9] = tuple.Item7.Item2;
                XlWorkSheet.Cells[LineIndex, 10] = tuple.Item7.Item3;
                LineIndex++;
                generation++;
            }
            SaveBestParticleFromCurrentIteration(topLineFromIteration, bestSolutionFromCurrentIteration);
        }

        private static void SaveBestParticleFromCurrentIteration(int topLineFromIteration,
            double[] bestSolutionFromCurrentIteration)
        {
            for (var solutionIndex = 0; solutionIndex < bestSolutionFromCurrentIteration.Count(); solutionIndex++)
            {
                var line = topLineFromIteration + 1 + solutionIndex;
                XlWorkSheet.Cells[line, 11] = bestSolutionFromCurrentIteration[solutionIndex];
                XlWorkSheet.Cells[line, 11].Interior.Color = XlRgbColor.rgbAliceBlue;
                XlWorkSheet.Cells[line, 11].Font.Color = XlRgbColor.rgbForestGreen;
            }
            XlWorkSheet.Range["F:F"].Columns.HorizontalAlignment = XlHAlign.xlHAlignCenter;
        }

        /// <summary>
        /// Seta configuração inicial e cria excel
        /// </summary>
        /// <param name="excelPath"></param>
        /// <param name="iteration"></param>
        /// <param name="misValue"></param>
        private static void Initialize(string excelPath, int iteration, object misValue)
        {
            if (iteration == 1)
            {
                if (File.Exists(excelPath))
                    File.Delete(excelPath);

                XlApp = new Application();
                XlWorkBook = XlApp.Workbooks.Add(misValue);
                XlWorkSheet = (Worksheet)XlWorkBook.Worksheets.Item[1];

                LineIndex = 1;
                BestAverageAndWorstFromIterations = new List<Tuple<double, double, double, double, double, double>>();
                BestTimeCost = Double.MaxValue;
            }


            XlWorkBook.CheckCompatibility = false;
            XlWorkBook.DoNotPromptForConvert = true;
            XlApp.DisplayAlerts = false;

            XlWorkSheet.Cells[LineIndex, 1] = "gen";
            XlWorkSheet.Cells[LineIndex, 2] = "BESTr";
            XlWorkSheet.Cells[LineIndex, 3] = "MEANr";
            XlWorkSheet.Cells[LineIndex, 4] = "WORSTr";
            XlWorkSheet.Cells[LineIndex, 5] = "SDr";

            XlWorkSheet.Cells[LineIndex, 6] = "BESTs";
            XlWorkSheet.Cells[LineIndex, 7] = "MEANs";
            XlWorkSheet.Cells[LineIndex, 8] = "WORSTs";
            XlWorkSheet.Cells[LineIndex, 9] = "SDs";
            
            XlWorkSheet.Cells[LineIndex, 10] = "ITERATION = " + iteration;
            XlWorkSheet.Cells[LineIndex+1, 10] = "seconds";

            LineIndex += 2;
            InitialCell = LineIndex;
        }

        /// <summary>
        /// Finaliza escrevendo as estatística finais, salvando o arquivo e libera o excel
        /// </summary>
        /// <param name="excelPath"></param>
        /// <param name="bestCost"></param>
        /// <param name="bestSolution"></param>
        /// <param name="calculations"></param>
        /// <param name="misValue"></param>
        /// <param name="targetPartile"></param>
        /// <param name="worstCost"></param>
        private static void Finalize(string excelPath, double bestCost, double[] bestSolution, List<double> std,
            Tuple<double, double, double, double, double, double, double> calculations, object misValue,
            string[] targetPartile, double worstCost)
        {
            SaveResults(bestCost, worstCost, std, calculations);

            SavePsoConfiguration();

            SaveBestAndTarget(bestSolution, targetPartile);

            AutoFitColumns();

            XlWorkBook.SaveAs(excelPath, XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue,
                XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);

            // Garbage collecting
            GC.Collect();
            GC.WaitForPendingFinalizers();

            // Clean up references to all COM objects
            // As per above, you're just using a Workbook and Excel Application instance, so release them:
            XlWorkBook.Close(true, misValue, misValue);
            XlApp.Quit();

            ReleaseObject(XlWorkSheet);
            ReleaseObject(XlWorkBook);
            ReleaseObject(XlApp);
            Marshal.FinalReleaseComObject(XlWorkBook);
            Marshal.FinalReleaseComObject(XlApp);

            KillSpecificExcelFileProcess();
        }

        /// <summary>
        /// Alinhar coluna ao conteúdo
        /// </summary>
        private static void AutoFitColumns()
        {
            //XlWorkSheet.Range["A:A"].Columns.AutoFit();
            //XlWorkSheet.Range["E:E"].Columns.AutoFit();
            //XlWorkSheet.Range["I:I"].Columns.AutoFit();
            //XlWorkSheet.Range["K:K"].Columns.AutoFit();
            //XlWorkSheet.Range["N:N"].Columns.AutoFit();
            //XlWorkSheet.Range["R:S"].Columns.AutoFit();
            //XlWorkSheet.Range["X:Y"].Columns.AutoFit();

            XlWorkSheet.Range["A:AD"].Columns.AutoFit();
        }

        /// <summary>
        /// Salva a Melhor partícula
        /// </summary>
        /// <param name="bestSolution"></param>
        /// <param name="targetPartile"></param>
        private static void SaveBestAndTarget(double[] bestSolution, string[] targetPartile)
        {
            XlWorkSheet.Cells[3, 29] = "Melhor Partícula";
            for (var solutionIndex = 0; solutionIndex < bestSolution.Count(); solutionIndex++)
            {
                XlWorkSheet.Cells[4 + solutionIndex, 29] = bestSolution[solutionIndex];
            }
            //Partícula Target
            XlWorkSheet.Cells[3, 30] = "Partícula Target";
            for (var solutionIndex = 0; solutionIndex < targetPartile.Count(); solutionIndex++)
            {
                XlWorkSheet.Cells[4 + solutionIndex, 30] = targetPartile[solutionIndex];
            }
        }

        /// <summary>
        /// Salva a Configuração do PSO
        /// </summary>
        private static void SavePsoConfiguration()
        {
            //XlWorkSheet.Range["M2:M2"].Cells.Interior.Color = XlRgbColor.rgbLightBlue;
            XlWorkSheet.Cells[2, 19] = "Configuração";
            XlWorkSheet.Cells[3, 19] = "População";
            XlWorkSheet.Cells[4, 19] = PsoConfiguration.PopulationSize;
            XlWorkSheet.Cells[3, 20] = "Gerações";
            XlWorkSheet.Cells[4, 20] = PsoConfiguration.NumberOfGenerations;
            XlWorkSheet.Cells[3, 21] = "Iterações";
            XlWorkSheet.Cells[4, 21] = PsoConfiguration.NumberOfIterations;
            XlWorkSheet.Cells[3, 22] = "Inércia";
            XlWorkSheet.Cells[4, 22] = PsoConfiguration.InitialInertia + "-" + PsoConfiguration.FinalInertia;
            XlWorkSheet.Cells[3, 23] = "Coef. Cognitivo";
            XlWorkSheet.Cells[4, 23] = PsoConfiguration.InitialAccelerationCoeficientTendencyToOwnBest + "-" +
                                       PsoConfiguration.FinalAccelerationCoeficientTendencyToOwnBest;
            XlWorkSheet.Cells[3, 24] = "Coef. Social";
            XlWorkSheet.Cells[4, 24] = PsoConfiguration.InitialAccelerationCoeficientTendencyToGlobalBest + "-" +
                                       PsoConfiguration.FinalAccelerationCoeficientTendencyToGlobalBest;

            XlWorkSheet.Cells[3, 25] = "SteadyState";
            XlWorkSheet.Cells[4, 25] = PsoConfiguration.NumberOfRunsWithSteadyState;
        }

        /// <summary>
        /// Salva os Resultados
        /// </summary>
        /// <param name="bestCost"></param>
        /// <param name="calculations"></param>
        /// <param name="worstCost"></param>
        private static void SaveResults(double bestCost, double worstCost, List<double> std,
            Tuple<double, double, double, double, double, double, double> calculations)
        {
            XlWorkSheet.Cells[1, 12] = "TARGET";
            XlWorkSheet.Cells[1, 13] = "Best";
            XlWorkSheet.Cells[1, 14] = "Best Average";
            XlWorkSheet.Cells[1, 15] = "Average";
            XlWorkSheet.Cells[1, 16] = "Worst Average";
            XlWorkSheet.Cells[1, 17] = "Best Time";

            XlWorkSheet.Cells[2, 12] = bestCost;
            XlWorkSheet.Cells[2, 13] = calculations.Item1;
            XlWorkSheet.Cells[2, 14] = calculations.Item2;
            XlWorkSheet.Cells[2, 15] = calculations.Item3;
            XlWorkSheet.Cells[2, 16] = calculations.Item4;
            XlWorkSheet.Cells[2, 17] = BestTimeCost;

            XlWorkSheet.Cells[4, 12] = "Fronteira";
            XlWorkSheet.Cells[4, 13] = "Worst";
            XlWorkSheet.Cells[4, 14] = "Best Average";
            XlWorkSheet.Cells[4, 15] = "Average";
            XlWorkSheet.Cells[4, 16] = "Worst Average";

            XlWorkSheet.Cells[5, 13] = worstCost;
            XlWorkSheet.Cells[5, 14] = calculations.Item5;
            XlWorkSheet.Cells[5, 15] = calculations.Item6;
            XlWorkSheet.Cells[5, 16] = calculations.Item7;

            XlWorkSheet.Cells[3, 17] = "STD R e S";
            XlWorkSheet.Cells[4, 17] = std.First();
            XlWorkSheet.Cells[5, 17] = std.Last();
        }

        /// <summary>
        /// Calcula a mlehor solução, a média dos melhores de cada iteração, 
        /// a média de todas as iterações e a média dos piores 
        /// </summary>
        /// <param name="bestSolution"></param>
        /// <param name="bestCostFromAll"></param>
        /// <returns></returns>
        private static Tuple<double, double, double, double, double, double, double> CalculateBestAverageAndWorstFromIterations(
            double bestSolution, double bestCostFromAll)
        {
            var best = Double.MaxValue;
            var bestError = Double.MaxValue;
            var bestAverage = 0.0;
            var average = 0.0;
            var worstAverage = 0.0;

            var bestAverageS = 0.0;
            var averageS = 0.0;
            var worstAverageS = 0.0;

            foreach (var tuple in BestAverageAndWorstFromIterations)
            {
                bestAverage += tuple.Item1;
                average += tuple.Item2;
                worstAverage += tuple.Item3;

                bestAverageS += tuple.Item4;
                averageS += tuple.Item5;
                worstAverageS += tuple.Item6;

                //salvo se for melhor resultado
                var error = Math.Abs(tuple.Item1 - bestSolution);
                if (error < bestError)
                {
                    bestError = error;
                    best = tuple.Item1;
                }

            }
            bestAverage /= BestAverageAndWorstFromIterations.Count;
            average /= BestAverageAndWorstFromIterations.Count;
            worstAverage /= BestAverageAndWorstFromIterations.Count;

            bestAverageS /= BestAverageAndWorstFromIterations.Count;
            averageS /= BestAverageAndWorstFromIterations.Count;
            worstAverageS /= BestAverageAndWorstFromIterations.Count;

            var bestAverageWorst = new Tuple<double, double, double, double, double, double, double>(bestCostFromAll, bestAverage,
                average, worstAverage, bestAverageS, averageS, worstAverageS);
            return bestAverageWorst;
        }


        /// <summary>
        /// This function is created to release the excel class object.
        /// </summary>
        /// <param name="obj"></param>
        private static void ReleaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Exception Occured while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }


        /// <summary>
        /// Criar o gráfico de evolução
        /// </summary>
        /// <param name="workSheet"></param>
        /// <param name="initialPoint"></param>
        /// <param name="finalPoint"></param>
        /// <param name="iteration"></param>
        private static void CreateChart(Worksheet workSheet, int initialPoint, int finalPoint, int iteration)
        {
            var xlCharts = (ChartObjects)workSheet.ChartObjects(Type.Missing);
            var myChart = xlCharts.Add(530, 80+((iteration-1)*500) + iteration, 800, 250);
            var chartPage = myChart.Chart;
            chartPage.HasTitle = true;
            chartPage.ChartTitle.Text = "Evolução";
            myChart.Select();
            
            chartPage.ChartType = XlChartType.xlLine;
            //chartPage.ChartType = XlChartType.xlXYScatterLines;

            SeriesCollection seriesCollection = chartPage.SeriesCollection();

            var series1 = seriesCollection.NewSeries();
            var initialCell = "B" + initialPoint.ToString(CultureInfo.InvariantCulture);
            var finalCell = "B" + finalPoint.ToString(CultureInfo.InvariantCulture);
            //series1.XValues = xlWorkSheet.get_Range("A1", "B1"); 
            series1.Values = workSheet.Range[initialCell, finalCell];
            series1.Name = "Melhor";

            var series2 = seriesCollection.NewSeries();
            initialCell = "C" + initialPoint.ToString(CultureInfo.InvariantCulture);
            finalCell = "C" + finalPoint.ToString(CultureInfo.InvariantCulture);
            //series2.XValues = xlWorkSheet.get_Range("A2", "B2"); 
            series2.Values = workSheet.Range[initialCell, finalCell];
            series2.Name = "Média";

            var series3 = seriesCollection.NewSeries();
            initialCell = "D" + initialPoint.ToString(CultureInfo.InvariantCulture);
            finalCell = "D" + finalPoint.ToString(CultureInfo.InvariantCulture);
            //series3.XValues = xlWorkSheet.get_Range("A3", "B3");
            series3.Values = workSheet.Range[initialCell, finalCell];
            series3.Name = "Pior";

            //Get the axes
            var xAxis = chartPage.Axes(XlAxisType.xlCategory, XlAxisGroup.xlPrimary) as Axis;
            if (xAxis == null) return;
            xAxis.HasTitle = true;
            xAxis.AxisTitle.Text = "Geração";
        }


        private static void CreateReferenceChart(Worksheet workSheet, int initialPoint, int finalPoint, int iteration)
        {
            var xlCharts = (ChartObjects)workSheet.ChartObjects(Type.Missing);
            var myChart = xlCharts.Add(530, 330 + ((iteration - 1) * 500) + iteration, 800, 250);
            var chartPage = myChart.Chart;
            chartPage.HasTitle = true;
            chartPage.ChartTitle.Text = "Evolução";
            myChart.Select();

            chartPage.ChartType = XlChartType.xlLine;
            //chartPage.ChartType = XlChartType.xlXYScatterLines;

            SeriesCollection seriesCollection = chartPage.SeriesCollection();

            var series1 = seriesCollection.NewSeries();
            var initialCell = "F" + initialPoint.ToString(CultureInfo.InvariantCulture);
            var finalCell = "F" + finalPoint.ToString(CultureInfo.InvariantCulture);
            //series1.XValues = xlWorkSheet.get_Range("A1", "B1"); 
            series1.Values = workSheet.Range[initialCell, finalCell];
            series1.Name = "Melhor";

            var series2 = seriesCollection.NewSeries();
            initialCell = "G" + initialPoint.ToString(CultureInfo.InvariantCulture);
            finalCell = "G" + finalPoint.ToString(CultureInfo.InvariantCulture);
            //series2.XValues = xlWorkSheet.get_Range("A2", "B2"); 
            series2.Values = workSheet.Range[initialCell, finalCell];
            series2.Name = "Média";

            var series3 = seriesCollection.NewSeries();
            initialCell = "H" + initialPoint.ToString(CultureInfo.InvariantCulture);
            finalCell = "H" + finalPoint.ToString(CultureInfo.InvariantCulture);
            //series3.XValues = xlWorkSheet.get_Range("A3", "B3");
            series3.Values = workSheet.Range[initialCell, finalCell];
            series3.Name = "Pior";

            //Get the axes
            var xAxis = chartPage.Axes(XlAxisType.xlCategory, XlAxisGroup.xlPrimary) as Axis;
            if (xAxis == null) return;
            xAxis.HasTitle = true;
            xAxis.AxisTitle.Text = "Geração";
        }

        public static void KillSpecificExcelFileProcess()
        {
            var processes = from p in Process.GetProcessesByName("EXCEL")
                            select p;

            foreach (var process in processes)
            {
                //if (process.MainWindowTitle == "Microsoft Excel") //MainWindowTitle está vindo com string vazia
                    process.Kill();
            }
        }
    }
}
