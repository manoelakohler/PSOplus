using System.Linq;
using PSO.Benchmarks.NonLinearConstraints;
using PSO.Interfaces;

namespace PSO.Benchmarks.LinearConstraints
{
    public class LinearConstraints : IConstraints
    {
        private readonly double _bounder;
        private readonly bool _specialBounder;

        public BenchmarksNames FunctionName { get; private set; }
        
        public LinearConstraints(double bounder)
        {
            _specialBounder = true;
            _bounder = bounder;
        }
        public LinearConstraints()
        {
            _specialBounder = false;
        }

        /// <summary>
        /// Verifica a validade da partícula
        /// </summary>
        /// <param name="particle"></param>
        /// <returns></returns>
        public virtual bool IsFeasible(IParticle particle)
        {
            if (_specialBounder == false)
            {
                return CheckBounders(particle) && CheckLinearConstraints(particle);
            }
            return CheckBounders2(particle);
        }

        public bool IsFeasible(IParticleNLC particle)
        {
            throw new System.NotImplementedException();
        }


        /// <summary>
        /// Fronteira para qualquer benhcmark original, sempre [-3,3].
        /// </summary>
        /// <param name="particle"></param>
        /// <returns></returns>
        private bool CheckBounders(IParticle particle)
        {
            if (particle.Position.All(x=> x >= -3) && particle.Position.All(x=> x <= 3))
                return true;
            return false;
        }

        /// <summary>
        /// Verificação das restrições lineares
        /// </summary>
        /// <param name="particle"></param>
        /// <returns></returns>
        private bool CheckLinearConstraints(IParticle particle)
        {
            //todo
            return true;
        }

        private bool CheckBounders2(IParticle particle)
        {
            return particle.Position.All(x => x >= -_bounder) && particle.Position.All(x => x <= _bounder);
        }

        public ConstraintViolationAndVariables GetConstraintViolation(double[] x)
        {
            throw new System.NotImplementedException();
        }
    }
}
