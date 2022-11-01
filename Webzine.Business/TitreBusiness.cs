using Abp.Domain.Uow;
using Webzine.Business.Contracts;
using Webzine.Entity;
using Webzine.Repository;
using Webzine.Repository.Contracts;

namespace Webzine.Business
{
    public class TitreBusiness : ITitreBusiness
    {
        private ITitreRepository _titreRepository;


        public TitreBusiness()
        {
            _titreRepository = new DbTitreRepository();
        }

        [UnitOfWork]
        public void IncrementLike(Titre titre)
        {
            titre.NbLikes++;
            _titreRepository.IncrementNbLikes(titre);
            _titreRepository.Save();

        }
        public void IncrementVue(Titre titre)
        {
            titre.NbLectures++;
            _titreRepository.IncrementNbLectures(titre);
            _titreRepository.Save();
        }
    }
}
