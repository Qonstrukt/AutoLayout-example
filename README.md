# AutoLayout-example
AutoLayout example used for the Dutch .NET Developers Meetup on March 10th, 2015

This project contains the following Storyboards:

###NoAutoLayout###
This Storyboard is setup with old-fanshioned "Springs and Struts". Layout is messed up on a lot of devices here.

###AutoLayout###
This Storyboard uses AutoLayout as is available since iOS 6. It also uses a custom TableViewCell which demonstrates that AutoLayout can also be used there, and that the height is automatically calculated since iOS 8.

###SizeClasses###
This Storyboard uses Size Classes, and allows the use of a SplitViewController, even on iPhone. It also employs a ScrollView in the DetailViewController to demonstrate how to use these. (Look carefully at the constraints the subview has to the ScrollView, and it's parent View.) And repositions items on the DetailViewController when there isn't much screen space available.

This Storyboard also employs the RootViewController subclass that alters traits to show the actual SplitViewController on the iPhone 6 instead of just the 6 Plus as is normally the case.
