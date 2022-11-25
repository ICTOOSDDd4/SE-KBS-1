namespace OpenPOS_APP.Resources.Controls;

public partial class Header : StackLayout
{
   public bool Showing { get; set; }

   private static readonly BindableProperty headerProperty = BindableProperty.Create(
        propertyName: nameof(Showing),
        returnType: typeof(bool),
        defaultValue: true,
        declaringType: typeof(Header),
        defaultBindingMode: BindingMode.OneWay);

    public Header()
	{
		InitializeComponent();
	}

   private void OnSearch(object sender, EventArgs e)
   {

   }

   private void OnSearchTextChanged(object sender, EventArgs e) { }


   private async void OnClickedAccount(object sender, EventArgs e)
   {
      await Shell.Current.DisplayAlert("Work In Progress", "This feature will be released soon, thank you for your patience.", "Understood");
   }
   private async void OnClickedCard(object sender, EventArgs e) 
   {
      await Shell.Current.GoToAsync(nameof(CheckoutOverview));
   }
}