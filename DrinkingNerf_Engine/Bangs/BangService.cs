using DrinkingNerf_Engine.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkingNerf_Engine.Bangs
{
    public class BangService
    {
        protected readonly IBangRepository _bangRepository;
        private readonly UserService _userService;

        public BangService(IBangRepository bangRepository, UserService userService)
        {
            _bangRepository = bangRepository;
            _userService = userService;
        }

        public void RegisterBangOutcome(BangOutcome bangOutcome)
        {
            _bangRepository.Add(bangOutcome);
        }

        public BangOutcome[] GetBangs() => _bangRepository.GetBangs().OrderByDescending(b => b.DateTime).ToArray();

        public BangOutcome[] GetBangsFromShooterId(UserId shooterId)
        {
            IEnumerable<BangOutcome> FilterBangs(IEnumerable<BangOutcome> bangs)
            {
                return bangs.Where(b => b.Shooter.Id == shooterId.Id);
            }

            return FilterBangs(_bangRepository.GetBangs()).OrderByDescending(b => b.DateTime).ToArray();
        }

        public BangOutcome[] GetBangsFromTargetId(UserId targetId)
        {
            IEnumerable<BangOutcome> FilterBangs(IEnumerable<BangOutcome> bangs)
            {
                return bangs.Where(b => b.Target.Id == targetId.Id);
            }

            return FilterBangs(_bangRepository.GetBangs()).OrderByDescending(b => b.DateTime).ToArray();
        }

        public BangOutcome[] GetTodaysBangs()
        {
            return GetBangs().Where(b => b.DateTime.Day == DateTime.Today.Day).ToArray();
        }

        public bool ShouldRegainAmmo(UserId shooterId)
        {
            var todayBangs = GetBangsFromShooterId(shooterId).Where(b => b.DateTime.Day == DateTime.Today.Day);


            return todayBangs.Aggregate(0, (acc, bang) => acc += (bang.Outcome == Bang.OutcomeEnum.Hit ? 1 : 0)) % RULE_SET.HitToRegainAmmo == 0;
        }

        public void DeleteBang(BangOutcome bang)
        {
            var shooterUser = _userService.GetUser(bang.Shooter);

            shooterUser.Score -= bang.ShooterHitScoreModificator;

            if (shooterUser.Ammunitions < RULE_SET.DefaultAmmo)
                shooterUser.Ammunitions++;

            _bangRepository.Delete(bang);
            _userService.UpdateUser(shooterUser);
        }
    }
}
