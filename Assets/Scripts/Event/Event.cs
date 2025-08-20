using System;

public class Event {
    public bool enableOption3Button = true;
    public bool enableOption2Button = true;
    public bool enableOption1Button = true;
    public Action OnSetup;
    public Action OnClickOption1;
    public Action OnClickOption2;
    public Action OnClickOption3;
}
