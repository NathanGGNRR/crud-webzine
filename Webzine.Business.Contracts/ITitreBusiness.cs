using Webzine.Entity;

namespace Webzine.Business.Contracts
{
    public interface ITitreBusiness
    {
        public void IncrementLike(Titre titre);
        public void IncrementVue(Titre titre);
    }
}
