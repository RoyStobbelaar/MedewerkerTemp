using System;
using System.IO;
using System.Diagnostics;
using System.Windows.Input;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace MCCForms
{
	public partial class MCCSelectImageViewCell : MCCBaseTitleViewCell
	{
		public ICommand ImageTapCommand { get; set; }

		public static readonly BindableProperty PathToImage1Property = BindableProperty.Create<MCCSelectImageViewCell, string>(item => item.PathToImage1, default(string), defaultBindingMode: BindingMode.TwoWay);
		public string PathToImage1 {
			get { 
				return (string)GetValue(PathToImage1Property);
			}
			set { 
				SetValue(PathToImage1Property, value); 
			}
		}

		public static readonly BindableProperty PathToImage2Property = BindableProperty.Create<MCCSelectImageViewCell, string>(item => item.PathToImage2, default(string), defaultBindingMode: BindingMode.TwoWay);
		public string PathToImage2 {
			get { 
				return (string)GetValue(PathToImage2Property);
			}
			set {
				SetValue(PathToImage2Property, value); 
			}
		}

		public static readonly BindableProperty PathToImage3Property = BindableProperty.Create<MCCSelectImageViewCell, string>(item => item.PathToImage3, default(string), defaultBindingMode: BindingMode.TwoWay);
		public string PathToImage3 {
			get { 
				return (string)GetValue(PathToImage3Property);
			}
			set { 
				SetValue(PathToImage3Property, value); 
			}
		}

		public static readonly BindableProperty ShowPlaceholder1Property = BindableProperty.Create<MCCSelectImageViewCell, bool>(item => item.ShowPlaceholder1, default(bool));
		public bool ShowPlaceholder1 {
			get {
				return (bool)GetValue(ShowPlaceholder1Property);
			}
			set {
				SetValue(ShowPlaceholder1Property, value); 
			}
		}

		public static readonly BindableProperty ShowPlaceholder2Property = BindableProperty.Create<MCCSelectImageViewCell, bool>(item => item.ShowPlaceholder2, default(bool));
		public bool ShowPlaceholder2 {
			get {
				return (bool)GetValue(ShowPlaceholder2Property);
			}
			set {
				SetValue(ShowPlaceholder2Property, value); 
			}
		}

		public static readonly BindableProperty ShowPlaceholder3Property = BindableProperty.Create<MCCSelectImageViewCell, bool>(item => item.ShowPlaceholder3, default(bool));
		public bool ShowPlaceholder3 {
			get {
				return (bool)GetValue(ShowPlaceholder3Property);
			}
			set {
				SetValue(ShowPlaceholder3Property, value); 
			}
		}

		public MCCSelectImageViewCell ()
		{
			InitializeComponent ();

			DetermineWhereToShowPlusImage ();

			if (DeviceTypeHelper.IsTablet) {
				labelTitle.FontSize = 15;
			}
		}

		void _imageViewTapped(object sender, EventArgs e)
		{
			if (sender == _imageView1 || sender == _imageView1Placeholder) {
				HandleImageClicked ("_imageView1");
			}else if (sender == _imageView2 || sender == _imageView2Placeholder) {
				HandleImageClicked ("_imageView2");
			} else if (sender == _imageView3 || sender == _imageView3Placeholder) {
				HandleImageClicked ("_imageView3");
			}
		}

		bool IsImage1Set()
		{
			return !string.IsNullOrEmpty (PathToImage1);
		}

		bool IsImage2Set()
		{
			return !string.IsNullOrEmpty (PathToImage2);
		}

		bool IsImage3Set()
		{
			return !string.IsNullOrEmpty (PathToImage3);
		}

		async void DetermineWhereToShowPlusImage()
		{
			//It takes some time before the binding is set - so delay for 100 milliseconds
			await Task.Delay (100);

			if (IsImage1Set ()) {
				ShowPlaceholder1 = false;
			} else {
				ShowPlaceholder1 = true;
			}

			if (IsImage2Set ()|| !IsImage1Set ()) {
				ShowPlaceholder2 = false;
			} else {
				ShowPlaceholder2 = true;
			}

			if (IsImage3Set () || !IsImage2Set () || !IsImage1Set ()) {
				ShowPlaceholder3 = false;
			} else {
				ShowPlaceholder3 = true;
			}
		}

		async void HandleImageClicked(string commandParamater)
		{
			try{
				int selectedImage = 0;
				if (commandParamater.Equals ("_imageView1")) {
					selectedImage = 1;
				} else if (commandParamater.Equals ("_imageView2")) {
					selectedImage = 2;
				} else if (commandParamater.Equals ("_imageView3")) {
					selectedImage = 3;
				}

				string titleText = "Maak uw keuze";
				string cancelText = "Annuleer";
				string choosePhotoText = "Foto kiezen uit album";
				string takePhotoText = "Nieuwe foto maken";
				string deletePhotoText = "Verwijder foto";
				string showPhotoText = "Toon foto";

				string actionsheetChoice = "";

				if(selectedImage == 1){
					if(IsImage1Set ()){
						actionsheetChoice = await DependencyService.Get<MessageVisualizerService> ().ShowActionSheet (titleText, showPhotoText, deletePhotoText, cancelText);
					}else {
						actionsheetChoice = await DependencyService.Get<MessageVisualizerService> ().ShowActionSheet (titleText, choosePhotoText, takePhotoText, cancelText);
					}
				} 
				else if(selectedImage == 2){
					if(IsImage2Set ()){
						actionsheetChoice = await DependencyService.Get<MessageVisualizerService> ().ShowActionSheet (titleText, showPhotoText, deletePhotoText, cancelText);
					}else {
						if(IsImage1Set ()){
							actionsheetChoice = await DependencyService.Get<MessageVisualizerService> ().ShowActionSheet (titleText, choosePhotoText, takePhotoText, cancelText);
						}
					}
				} 
				else if(selectedImage == 3){
					if(IsImage3Set ()){
						actionsheetChoice = await DependencyService.Get<MessageVisualizerService> ().ShowActionSheet (titleText, showPhotoText, deletePhotoText, cancelText);
					}else {
						if(IsImage2Set ()){
							actionsheetChoice = await DependencyService.Get<MessageVisualizerService> ().ShowActionSheet (titleText, choosePhotoText, takePhotoText, cancelText);
						}
					}
				}

				if(actionsheetChoice.Equals (choosePhotoText) || actionsheetChoice.Equals (takePhotoText))
				{
					string temporaryPath;
					if (actionsheetChoice.Equals (choosePhotoText)) {
						temporaryPath = await DependencyService.Get<IMediaPicker> ().GetPathToPictureFromGallery();
					} else {
						temporaryPath = await DependencyService.Get<IMediaPicker> ().GetPathToPictureFromCamera();
					}

					if (temporaryPath != null) 
					{
						var bytes = File.ReadAllBytes (temporaryPath);

						var destinationPath = Path.Combine (DocumentConstants.DocumentsPath, Path.GetFileName (temporaryPath));

						FilesystemHelper.WriteBytes (Path.GetFileName (temporaryPath), bytes);

						if (File.Exists (destinationPath)) {

							if (selectedImage == 1) {
								PathToImage1 = destinationPath;
							}
							else if (selectedImage == 2) {
								PathToImage2 = destinationPath;
							}
							else if (selectedImage == 3) {
								PathToImage3 = destinationPath;
							}
						}

						//Delete temporary (unencrypted) file - this file is stored by the mediapicker class (IMediaPicker implementation)
						File.Delete (temporaryPath);
					}
				}
				else if(actionsheetChoice.Equals (showPhotoText)){

					string pathToImageToOpen = "";
					if (selectedImage == 1) {
						pathToImageToOpen = PathToImage1;
					} else if (selectedImage == 2) {
						pathToImageToOpen = PathToImage2;
					} else if (selectedImage == 3) {
						pathToImageToOpen = PathToImage3;
					}

					await DependencyService.Get<NavigationService> ().GoToPage (new ImageViewerPage ("Afbeelding", pathToImageToOpen));
				}
				else if(actionsheetChoice.Equals (deletePhotoText)){

					if (selectedImage == 1) {
						File.Delete (PathToImage1);

						if (PathToImage3 != null) {
							PathToImage1 = PathToImage2;
							PathToImage2 = PathToImage3;
							PathToImage3 = null;
						} 
						else if (PathToImage2 != null) {
							PathToImage1 = PathToImage2;
							PathToImage2 = null;
						}
						else if (PathToImage1 != null) {
							PathToImage1 = null;
						}
					} 
					else if (selectedImage == 2) 
					{
						File.Delete (PathToImage2);

						if (PathToImage3 != null) {
							PathToImage2 = PathToImage3;
							PathToImage3 = null;
						} else if (PathToImage2 != null) {
							PathToImage2 = null;
						}
					} 
					else if (selectedImage == 3) 
					{
						File.Delete (PathToImage3);

						PathToImage3 = null;
					}
				}

				DetermineWhereToShowPlusImage();
			} 
			catch (Exception ex){
				Debug.WriteLine (ex.Message);
			}
		}
	}
}