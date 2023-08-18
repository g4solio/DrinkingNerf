using DrinkingNerf_Engine.Users;

namespace DrinkingNerf_Engine.Bangs
{
    public class BangOutcome
    {
        public UserId Shooter { get; set; }

        public UserId Target { get; set; }

        public Bang.OutcomeEnum Outcome { get; set; }

        public int ShooterHitScoreModificator { get; set; }

        public DateTime DateTime { get; set; }
    }
}