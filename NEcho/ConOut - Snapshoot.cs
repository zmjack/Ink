using System;

namespace NEcho
{
    public partial class ConOut
    {
        public ConColor ConColor
        {
            get
            {
                return new ConColor
                {
                    BackgroundColor = Console.BackgroundColor,
                    ForegroundColor = Console.ForegroundColor,
                };
            }
            set
            {
                Console.BackgroundColor = value.BackgroundColor;
                Console.ForegroundColor = value.ForegroundColor;
            }
        }

    }
}
