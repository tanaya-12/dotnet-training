using System;
namespace DelegateDemoApp
{
   public class OnClickEvenetArgs : EventArgs
    {
        public string ButtonName { get; set;}

    }
    public class Button
    {
        public delegate void OnClickHandler();

        public event OnClickHandler OnClick;

        public void Click()
        {
            OnClick?.Invoke();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Button btn = new Button();

            // Subscribe to the event
            btn.OnClick += HandleButtonClick;

            // Trigger the event
            btn.Click();

            Console.ReadLine();
        }

        // Event handler method
        static void HandleButtonClick()
        {
            Console.WriteLine("OnClick event handled in Main");
        }
    }
}

   