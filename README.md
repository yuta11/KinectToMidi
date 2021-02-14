# KinectToMidi
In general, this app receives Kinect data as input and sends MIDI signals as output. With using Kinect to Midi, you can build some collection of conditions and send MIDI signals if skeleton points’ coordinates correspond all these specified conditions. 
Conditions
Within a single block, you can add multiple conditions. To do that you have to select appropriate condition and press clip_image002 button.
Each condition can be one of two types: 
·         Skeleton point to coordinate
·         Skeleton point to skeleton point
All coordinates are in meters.
With using skeleton point to coordinate condition type you can specify some cuboid and joint (skeleton point) whose coordinates are compared with the cuboid. So the X, Y, Z values are coordinates of the left, bottom, nearest to Kinect cuboid corner and Height, Width and Length values are sizes on Y, X and Z axis correspondingly. For example if you add condition where Joint=HandRight, X=0.3, Y=0.5, Z=0, Height=2, Width=2 and Length=2, than specified signal will be sent only when you raise your hand up and right (if you located in the center front of the Kinect). 
With skeleton point to skeleton point condition type, it is possible to compare two skeleton joints with some specified radius and shift. The X, Y and Z values are the shift of second joint’s coordinates and Radius value is a radius of sphere around this shifted point. With using condition where First joint=HandRight, Second joint=Head, X=Y=Z=0 and Radius=0.2 specified signal will be sent when you bring your right hand to the head. 

Midi
If all conditions in the block were met, spicified MIDI signals are sent. Each of these signals can be one of two types:
·         CC (Control Change)
MIDI Note
By specifying event type, you can define when the signal should be sent:

·         In – the signal will be sent right after all conditions in the block are met.

·         Over – the signal will be sent each time when the program receives updated skeleton joints coordinates if these coordinates corresponds specified conditions.

·         Out – signal will be sent if all conditions were met a moment ago but now they are not.
Expressions
For minimum and maximum values of MIDI CC instead of constant values it’s possible to use some simple mathematical equations with using X, Y and Z coordinates of specified joint (for example: (2 + (X^10)*Y)/Z )
Other features
·         Saving/loading all blocks and MIDI settings to/from a file. 
·         Possibility to specify MIDI port that should be used with Kinect to MIDI.
·         Possibility to open Kinect depth/skeleton stream in separate window. Press Enter when the window is focused to switch it in the full screen mode and Esc key to return back.

 

Prerequisites
Windows 7, Windows 8, Windows 10
Kinect for Windows 
Kinect for Windows Runtime v1.8 
.NET Framework 4.5 

 

