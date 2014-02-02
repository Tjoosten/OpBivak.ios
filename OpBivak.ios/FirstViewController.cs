﻿using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.MessageUI;

namespace OpBivak.ios
{
	public partial class FirstViewController : UIViewController
	{
		public FirstViewController (IntPtr handle) : base (handle)
		{
			Title = NSBundle.MainBundle.LocalizedString ("First", "First");
			TabBarItem.Image = UIImage.FromBundle ("first");
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		#region View lifecycle

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			// Perform any additional setup after loading the view, typically from a nib.
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);
		}

		public override void ViewWillDisappear (bool animated)
		{
			base.ViewWillDisappear (animated);
		}

		public override void ViewDidDisappear (bool animated)
		{
			base.ViewDidDisappear (animated);
		}
		partial void btnCallKipdorp_TouchUpInside (UIButton sender){
			//not yet implemented
			string phone = "+32476588722";
			var urlToSend = new NSUrl ("telprompt://" + phone);
			if (UIApplication.SharedApplication.CanOpenUrl(urlToSend)){
				UIApplication.SharedApplication.OpenUrl(urlToSend);
			} else {
				UIAlertView alert = new UIAlertView();
				alert.Title = "Foutje";
				alert.AddButton("OK");
				alert.Message = "Dit toestel heeft heeft geen telefoon functionaliteit. Zoek een telefoon en bel " + phone + "voor de bivakpermanentie";
				alert.Show();

			}
			// set recipient for call.
			string[] recipient = new string[1] {"+32476588722"};
			//get name etc from settings
			//NSUserDefaults.StandardUserDefaults.SetValueForKey("John", "firstNameKey")
			string firstname = NSUserDefaults.StandardUserDefaults.StringForKey("firstNameKey");
			string lastname = NSUserDefaults.StandardUserDefaults.StringForKey("lastNameKey");
			string stamnummer = NSUserDefaults.StandardUserDefaults.StringForKey("stamNummerKey");
			string groep = NSUserDefaults.StandardUserDefaults.StringForKey("chiroGroepKey");
			string gemeente = NSUserDefaults.StandardUserDefaults.StringForKey("groepGemeenteKey");

			//build the message
			string message = "Ik ben ";
			if (!string.IsNullOrWhiteSpace(firstname) && !string.IsNullOrWhiteSpace(lastname)){
				message += firstname + " " + lastname;
			}else if (!string.IsNullOrWhiteSpace(firstname)){
				message += firstname;
			}else if (!string.IsNullOrWhiteSpace(lastname)){
				message += lastname;
			}
			if (!string.IsNullOrWhiteSpace(stamnummer)|| !string.IsNullOrWhiteSpace(groep) || !string.IsNullOrWhiteSpace(gemeente)){
				message += ", mijn Chirogroep is: " ;
			}
			if (!string.IsNullOrWhiteSpace(stamnummer)){
				message += stamnummer + " - ";
			}
			if (!string.IsNullOrWhiteSpace(groep)){
				message += groep + " ";
			}
			if (!string.IsNullOrWhiteSpace(gemeente) && string.IsNullOrWhiteSpace(groep) && string.IsNullOrWhiteSpace(stamnummer)){
				message += "uit " + gemeente + " ";
			}else if (!string.IsNullOrWhiteSpace(gemeente) && string.IsNullOrWhiteSpace(groep)){
				message += gemeente + " ";
			}else if (!string.IsNullOrWhiteSpace(gemeente)){
				message += "(" + gemeente + ") ";
			}
			message += "en ik belde de bivakpermanentie";


			//send the message
			ShowAndSendSMS(recipient, message);
		}
		public bool ShowAndSendSMS(string[] recipients, string body){
			if (MFMessageComposeViewController.CanSendText) {
				MFMessageComposeViewController message = new MFMessageComposeViewController ();
				message.Finished += (sender, e) => {
					message.DismissViewController (true, null);
				};
				message.Body = body;
				message.Recipients = recipients;
				this.PresentViewController (message, false, null);
			} else {
				//Oh oh
				UIAlertView alert = new UIAlertView();
				alert.Title = "Foutje";
				alert.AddButton("OK");
				alert.Message = "SMS sturen niet mogelijk...";
				alert.Show();
			}
			return true;
		}

		#endregion
	}
}

