<p style="margin:0cm 0cm 8pt"><span lang="EN-US" style="font-size:14pt; line-height:107%">Summary</span></p>
<p style="margin:0cm 0cm 8pt"><span lang="EN-US">In general, this app receives Kinect data as input and sends MIDI signals as output. With using Kinect to Midi, you can build some collection of conditions and send MIDI signals if skeleton points’ coordinates
 correspond all these specified conditions. </span></p>
<p style="margin:0cm 0cm 8pt"><img title="image" border="0" alt="image" src="https://user-images.githubusercontent.com/7378481/107875994-513ce700-6ed4-11eb-93b5-2426d1f6c469.jpg" width="690" height="410" style="border-left-width:0px; border-right-width:0px; border-bottom-width:0px; padding-top:0px; padding-left:0px; display:inline; padding-right:0px; border-top-width:0px"></p>
<p style="margin:0cm 0cm 8pt">&nbsp;</p>
<p style="margin:0cm 0cm 8pt"><span lang="EN-US" style="font-size:14pt; line-height:107%">Conditions</span></p>
<p style="margin:0cm 0cm 8pt">Within a single block, you can add multiple conditions. To do that you have to select appropriate condition and press "+" button on the panel.</p>
<p style="margin:0cm 0cm 8pt"><span lang="EN-US">Each condition can be one of two types:
</span></p>
<p style="margin:0cm 0cm 0pt 36pt; text-indent:-18pt"><span lang="EN-US" style="font-family:symbol">·<span style="font:7pt 'Times New Roman'">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span><span lang="EN-US">Skeleton point to coordinate</span></p>
<p style="margin:0cm 0cm 8pt 36pt; text-indent:-18pt"><span lang="EN-US" style="font-family:symbol">·<span style="font:7pt 'Times New Roman'">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span><span lang="EN-US">Skeleton point to skeleton point</span></p>
<p style="margin:0cm 0cm 8pt"><span lang="EN-US">All coordinates are in meters.</span></p>
<p>With using skeleton point to coordinate condition type you can specify some cuboid and joint (skeleton point) whose coordinates are compared with the cuboid. So the X, Y, Z values are coordinates of the left, bottom, nearest to Kinect cuboid corner and Height,
 Width and Length values are sizes on Y, X and Z axis correspondingly. For example if you add condition where Joint=HandRight, X=0.3, Y=0.5, Z=0, Height=2, Width=2 and Length=2, than specified signal will be sent only when you raise your hand up and right (if
 you located in the center front of the Kinect). </p>
<p style="margin:0cm 0cm 8pt"><img title="image" border="0" alt="image" src="https://user-images.githubusercontent.com/7378481/107875998-526e1400-6ed4-11eb-8946-b9c2e02a6c01.jpg" width="542" height="251" style="border-left-width:0px; border-right-width:0px; border-bottom-width:0px; padding-top:0px; padding-left:0px; display:inline; padding-right:0px; border-top-width:0px"></p>
<p>With skeleton point to skeleton point condition type, it is possible to compare two skeleton joints with some specified radius and shift. The X, Y and Z values are the shift of second joint’s coordinates and Radius value is a radius of sphere around this
 shifted point. With using condition where First joint=HandRight, Second joint=Head, X=Y=Z=0 and Radius=0.2 specified signal will be sent when you bring your right hand to the head.
</p>
<p style="margin:0cm 0cm 8pt"><img title="image" border="0" alt="image" src="https://user-images.githubusercontent.com/7378481/107875997-526e1400-6ed4-11eb-9811-f704ef715d86.jpg" width="548" height="244" style="border-left-width:0px; border-right-width:0px; border-bottom-width:0px; padding-top:0px; padding-left:0px; display:inline; padding-right:0px; border-top-width:0px"></p>
<p style="margin:0cm 0cm 8pt">&nbsp;</p>
<p style="margin:0cm 0cm 8pt"><span lang="EN-US" style="font-size:14pt; line-height:107%">Midi</span></p>
<p style="margin:0cm 0cm 8pt"><span lang="EN-US">If all conditions in the block were met, spicified MIDI signals are sent. Each of these signals can be one of two types:</span></p>
<p style="margin:0cm 0cm 0pt 36pt; text-indent:-18pt"><span lang="EN-US"><strong>CC</strong> (Control Change)</span></p>
<p style="margin:0cm 0cm 0pt 36pt; text-indent:-18pt"><img title="image" border="0" alt="image" src="https://user-images.githubusercontent.com/7378481/107875996-51d57d80-6ed4-11eb-937a-457e5d80bc53.jpg" width="529" height="229" style="border-left-width:0px; border-right-width:0px; border-bottom-width:0px; padding-top:0px; padding-left:0px; display:inline; padding-right:0px; border-top-width:0px"></p>
<p style="margin:0cm 0cm 8pt 36pt; text-indent:-18pt"><span lang="EN-US" style="font-family:symbol">·<span style="font:7pt 'Times New Roman'">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span><span lang="EN-US"><strong>MIDI Note</strong></span></p>
<p style="margin:0cm 0cm 8pt 36pt; text-indent:-18pt"><img title="image" border="0" alt="image" src="https://user-images.githubusercontent.com/7378481/107875995-51d57d80-6ed4-11eb-8525-f1cd72edb984.jpg" width="532" height="235" style="border-left-width:0px; border-right-width:0px; border-bottom-width:0px; padding-top:0px; padding-left:0px; display:inline; padding-right:0px; border-top-width:0px"></p>
<p style="margin:0cm 0cm 8pt"><span lang="EN-US">By specifying event type, you can define when the signal should be sent:</span></p>
<p style="margin:0cm 0cm 0pt 36pt; text-indent:-18pt"><span lang="EN-US" style="font-family:symbol">·<span style="font:7pt 'Times New Roman'">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span><strong><span lang="EN-US">In</span></strong><span lang="EN-US"> – the signal will be sent right after all conditions in the block are met.</span></p>
<p style="margin:0cm 0cm 0pt 36pt; text-indent:-18pt"><span lang="EN-US" style="font-family:symbol">·<span style="font:7pt 'Times New Roman'">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span><strong><span lang="EN-US">Over</span></strong><span lang="EN-US"> – the signal will be sent each time when the program receives updated skeleton joints coordinates if these coordinates corresponds specified conditions.</span></p>
<p style="margin:0cm 0cm 8pt 36pt; text-indent:-18pt"><span lang="EN-US" style="font-family:symbol">·<span style="font:7pt 'Times New Roman'">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span><strong><span lang="EN-US">Out</span></strong><span lang="EN-US"> – signal will be sent if all conditions were met a moment ago but now they are not.</span></p>
<p style="margin:0cm 0cm 8pt">&nbsp;</p>
<p style="margin:0cm 0cm 8pt"><span lang="EN-US" style="font-size:14pt; line-height:107%">Expressions</span></p>
<p style="margin:0cm 0cm 8pt"><span lang="EN-US">For minimum and maximum values of MIDI CC instead of constant values it’s possible to use some simple mathematical equations with using X, Y and Z coordinates of specified joint (for example:<em> (2 &#43; (X^10)*Y)/Z</em>
 )</span></p>
<p style="margin:0cm 0cm 8pt">&nbsp;</p>
<p style="margin:0cm 0cm 8pt"><span lang="EN-US" style="font-size:14pt; line-height:107%">Other features</span></p>
<p style="margin:0cm 0cm 0pt 36pt; text-indent:-18pt"><span lang="EN-US" style="font-family:symbol">·<span style="font:7pt 'Times New Roman'">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span><span lang="EN-US">Saving/loading all blocks and MIDI settings to/from a file.
</span></p>
<p style="margin:0cm 0cm 0pt 36pt; text-indent:-18pt"><span lang="EN-US" style="font-family:symbol">·<span style="font:7pt 'Times New Roman'">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span><span lang="EN-US">Possibility to specify MIDI port that should be used with Kinect to MIDI.</span></p>
<p style="margin:0cm 0cm 8pt 36pt; text-indent:-18pt"><span lang="EN-US" style="font-family:symbol">·<span style="font:7pt 'Times New Roman'">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span><span lang="EN-US">Possibility to open Kinect depth/skeleton stream in separate window. Press Enter when the window is focused to switch it in the full screen mode and Esc key to return back.</span></p>
<p style="margin:0cm 0cm 8pt">&nbsp;</p>
<p style="margin:0cm 0cm 8pt"><span lang="EN-US" style="font-size:14pt; line-height:107%">Prerequisites</span></p>
<ul>
<li><span lang="EN-US" style="text-indent:-18pt">Windows 7, Windows 8, Windows 10</span>
</li><li><span style="background-color:transparent">Kinect for Windows</span> </li><li><span style="background-color:transparent">Kinect for Windows Runtime v1.8 </span>
</li><li><span style="background-color:transparent"></span><span style="background-color:transparent">.NET Framework 4.5</span>
</li><li><span style="background-color:transparent"></span><span style="background-color:transparent">Kinect v1 connected</span>
</li></ul>
<p style="margin:0cm 0cm 8pt">&nbsp;</p>
<p style="margin:0cm 0cm 8pt"><span lang="EN-US" style="font-size:14pt; line-height:107%">P.S.</span></p>
<p style="margin:0cm 0cm 8pt"><span lang="EN-US">I created this app for my own use years ago, but fill free to contact me about new features or issues.</span></p>
</div><div class="ClearBoth">
