namespace DefaultNamespace
{
    public class DigitState
    {
        public DigitScript DigitScript { get; set; }
        public int CurrentValue { get; set; }

        public DigitState(DigitScript digitScript, int currentValue)
        {
            DigitScript = digitScript;
            CurrentValue = currentValue;
        }
    }
}
