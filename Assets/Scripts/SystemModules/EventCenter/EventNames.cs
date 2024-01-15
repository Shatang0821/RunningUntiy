public static class StateEvents
{
    public const string Initialize = "Init";

    public const string Playing = "Playing";

    public const string SpawnPlayer = "Respawn";

    public const string SetCustomJumpForce = "SetCustomJumpForce";
}

public static class ButtonEvents
{
    public const string startButton = "StartButton";

    public const string optionButton = "OptionButton";

    public const string quitButton = "QuitButton";

    public const string resumeButton = "ResumeButton";

    public const string mainMenuButton = "MainMenuButton";
}

public static class InputEvents
{
    public const string disableAllInput = "disableInput";

    //�ȉ��l�p�ҏW
    public const string EnablePauseMenuInput = "Switch InputMap for UI";
    public const string EnableGameInput = "Switch InputMap for Game";
    public const string DynamicInput = "Switch Input Update Mode to Dynamic";
    public const string FixedInput = "Switch Input Update Mode to Fixed";

    public const string GamepadVibration = "pad Virate";
    public const string StopGamepadVibration = "stop pad Virate";
}

public static class TimeEvents
{
    // �Q�[���̎��Ԃ��~����C�x���g��\��������
    public const string StopTime = "Set timesclae to 0";

    // �Q�[���̎��Ԃ��ĊJ����C�x���g��\��������
    public const string startTime = "Set timesclae to return";
}

public static class UIEvents
{
    //�Q�[�����ꎞ��~���̃��j���[�o�[�̕\��
    public const string ShowMenuBar = "Show UI menu bar in game";
    //�Q�[�����ꎞ��~���̃��j���[�o�[�̔�\��
    public const string HideMenuBar = "Hide UI Menu bar in game";
}