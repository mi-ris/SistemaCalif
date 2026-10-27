namespace SistemaCalif;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        // Configurar Page1 como la página principal al abrir la app
        MainPage = new SistemaCalif.Views.Pagina();
    }
}