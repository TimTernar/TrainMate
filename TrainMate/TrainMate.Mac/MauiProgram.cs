namespace TrainMate.Mac
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            builder
                .UseSharedMauiApp();
            //to je moj backup
            return builder.Build();
        }
    }
}
