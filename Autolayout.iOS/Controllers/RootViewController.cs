using System;
using CoreGraphics;
using UIKit;

namespace Autolayout.iOS
{
	public partial class RootViewController : UIViewController
	{
		private const float iPhone6Width = 375;


		public RootViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			if (View.Frame.Width > iPhone6Width) {
				UpdateTraits (UITraitCollection.FromHorizontalSizeClass (UIUserInterfaceSizeClass.Regular));
			} else {
				UpdateTraits (UITraitCollection.FromHorizontalSizeClass (UIUserInterfaceSizeClass.Compact));
			}
		}

		public override void ViewWillTransitionToSize (CGSize toSize, IUIViewControllerTransitionCoordinator coordinator)
		{
			base.ViewWillTransitionToSize (toSize, coordinator);

			// iPhone 6 and bigger
			if (toSize.Width > iPhone6Width) {
				UpdateTraits (UITraitCollection.FromHorizontalSizeClass (UIUserInterfaceSizeClass.Regular));
			} else {
				UpdateTraits (UITraitCollection.FromHorizontalSizeClass (UIUserInterfaceSizeClass.Compact));
			}
		}

		protected void UpdateTraits (UITraitCollection traitCollection)
		{
			SetOverrideTraitCollection (traitCollection, ChildViewControllers [0]);
		}
	}
}
