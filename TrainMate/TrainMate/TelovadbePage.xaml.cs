namespace TrainMate;

public partial class TelovadbePage : ContentPage
{
	public TelovadbePage()
	{
		InitializeComponent();
	}

    private int _counter = 2;

	private void AddWorkout(object sender, EventArgs e)
	{
		WorkoutSelection.Add(new TextCell
		{
			Text = $"{_counter}. New workout",
             Detail = "Dynamically added row"

        });

        _counter++;


    }

}