using System.Linq;
using PSO.Interfaces;
using PSO.Particles;

namespace PSO.Benchmarks.LinearConstraints
{
    public class LinearConstraints : IConstraints
    {
        /// <summary>
        /// Verifica a validade da partícula
        /// </summary>
        /// <param name="particle"></param>
        /// <returns></returns>
        public virtual bool IsFeasible(IParticle particle)
        {
            return CheckBounders(particle) && CheckLinearConstraints(particle);
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
    }
}
