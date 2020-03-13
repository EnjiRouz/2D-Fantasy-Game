using UnityEngine;

static public class QuestActivators {
    public enum Activators
    {
        PressKeyE,  // Нажатие кнопки E
        GoTop,      // Движение вверх (W, стрелка вверх)
        GoRight,    // Движение вправо (D, стрелка вправо) 
        GoBottom,
        GoLeft,
        Enter       // Приближение у объекту (автоматически, без нажатия)
    }

    static public bool Check(Activators activator)
    {
        switch (activator)
        {
            case Activators.PressKeyE:
                return Input.GetKeyDown(KeyCode.E);
            case Activators.GoTop:
                return Input.GetAxis("Vertical") > 0;
            case Activators.GoRight:
                return Input.GetAxis("Horizontal") > 0;
            case Activators.GoBottom:
                return Input.GetAxis("Vertical") < 0;
            case Activators.GoLeft:
                return Input.GetAxis("Horizontal") < 0;
            case Activators.Enter:
                return true;
            default:
                return false;
        }
    }
}
