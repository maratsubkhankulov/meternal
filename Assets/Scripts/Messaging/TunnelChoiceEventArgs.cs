using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class TunnelChoiceEventArgs : MessageEventArgs
{
	public TunnelSection.Directions direction;
	public Boolean correctChoice;
    public TunnelChoiceEventArgs(TunnelSection.Directions _direction, bool _correctChoice)
    {
		direction = _direction;
		correctChoice = _correctChoice;
    }
}