namespace TrainMate;

public partial class ZgodovinaPage : ContentPage
{
	public ZgodovinaPage()
	{
		InitializeComponent();
        BindingContext = new ZgodovinaViewModel();
    }
}