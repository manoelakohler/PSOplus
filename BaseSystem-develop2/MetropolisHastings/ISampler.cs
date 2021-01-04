using System.Threading;

namespace Octopus.Engine.Services.RandomGenerator
{
    public interface ISampler<T>
    {
        /// <summary>
        /// Gera uma amostra aleatória da distribuição.
        /// Recebe um token para interromper a otimização se ela tiver sido cancelada. 
        /// </summary>
        T Sample(CancellationToken token, string studyName, string reservoirName, int reservoirIdOnStudy, int? noWellRegionId);

        /// <summary>
        /// Gera uma amostra aleatória da distribuição condicional, dada a última amostra gerada.
        /// </summary>
        T Sample(T sample, CancellationToken token, string studyName, string reservoirName, int reservoirIdOnStudy, int? noWellRegionId);

        /// <summary>
        /// Gera uma amostra igual à semente incial passada mais os poços fixos do estudo.
        /// </summary>
        T Sample(CancellationToken token, string studyName, string reservoirName, int reservoirIdOnStudy,
            int? noWellRegionId, T initialSeed);
        

        /// <summary>
        /// Computa um valor diretamente proporcional a verossimilhança da amostra de variável
        /// aleatória fornecida como parâmetro.
        /// Caso a distribuição seja discreta, este método retorna um valor proporcional a própria
        /// probabilidade de ocorrência da amostra.
        /// Recebe um token para interromper a otimização se ela tiver sido cancelada.
        /// </summary>
        double GetLikelihood(T sample, CancellationToken token);

        /// <summary>
        /// Computa um valor diretamente proporcional a verossimilhança da amostra de variável
        /// aleatória fornecida como parâmetro, condicionada ao conhecimento de uma segunda variável.
        /// Caso a distribuição seja discreta, este método retorna um valor proporcional a própria
        /// probabilidade de ocorrência da amostra.
        /// </summary>
        double GetLikelihood(T sample, T givenSample, CancellationToken token);
    }
}