namespace SistemaCalif.Views;

public partial class Pagina : ContentPage
{
    public Pagina()
    {
        InitializeComponent();
    }

    private async void OnCalcularClicked(object sender, EventArgs e)
    {
        // 1. Validar selección del estudiante
        if (pickerEstudiantes.SelectedItem == null)
        {
            await DisplayAlert("Atención del Sistema", "Por favor seleccione un estudiante del listado oficial.", "Aceptar");
            return;
        }

        string estudianteSeleccionado = pickerEstudiantes.SelectedItem.ToString() ?? "Desconocido";

        // 2. Validar rangos numéricos estrictos (0.1 a 10)
        if (!double.TryParse(txtSeguimiento1.Text, out double seg1) || seg1 < 0.1 || seg1 > 10 ||
            !double.TryParse(txtExamen1.Text, out double ex1) || ex1 < 0.1 || ex1 > 10 ||
            !double.TryParse(txtSeguimiento2.Text, out double seg2) || seg2 < 0.1 || seg2 > 10 ||
            !double.TryParse(txtExamen2.Text, out double ex2) || ex2 < 0.1 || ex2 > 10)
        {
            await DisplayAlert("Error de Validación", "Todas las calificaciones deben ser numéricas y encontrarse estrictamente en el rango de 0.1 a 10.", "Aceptar");
            return;
        }

        // 3. Obtener la fecha seleccionada de forma segura
        DateTime fechaValue = datePickerFecha.Date ?? DateTime.Now;
        string fechaSeleccionada = fechaValue.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

        // 4. Cálculos de notas parciales y final
        double parcial1 = (seg1 * 0.3) + (ex1 * 0.2);
        double parcial2 = (seg2 * 0.3) + (ex2 * 0.2);
        double notaFinal = parcial1 + parcial2;

        // 5. Determinar Estado según las reglas del requerimiento
        string estado = "";
        if (notaFinal >= 7)
        {
            estado = "Aprobado";
        }
        else if (notaFinal >= 5 && notaFinal <= 6.9)
        {
            estado = "Complementario";
        }
        else
        {
            estado = "REPROBADO";
        }

        // 6. Actualizar el Panel Visual en la interfaz (Dashboard del Sistema)
        lblResNombre.Text = estudianteSeleccionado;
        lblResParcial1.Text = parcial1.ToString("F2");
        lblResParcial2.Text = parcial2.ToString("F2");
        lblResNotaFinal.Text = notaFinal.ToString("F2");
        lblResEstado.Text = estado.ToUpper();
        cardResultados.IsVisible = true; // Muestra la tarjeta de resultados dinámicamente

        // 7. Mensaje emergente requerido usando "\n"
        string mensaje = $"Nombre: {estudianteSeleccionado}\n" +
                         $"Fecha: {fechaSeleccionada}\n" +
                         $"Nota Parcial 1: {parcial1:F2}\n" +
                         $"Nota Parcial 2: {parcial2:F2}\n" +
                         $"Nota Final: {notaFinal:F2}\n" +
                         $"Estado: {estado}";

        await DisplayAlert("SISTEMA DE CALIFICACIONES - REGISTRO EXITOSO", mensaje, "Aceptar");
    }

    private void OnLimpiarClicked(object sender, EventArgs e)
    {
        pickerEstudiantes.SelectedIndex = -1;
        txtSeguimiento1.Text = string.Empty;
        txtExamen1.Text = string.Empty;
        txtSeguimiento2.Text = string.Empty;
        txtExamen2.Text = string.Empty;
        datePickerFecha.Date = DateTime.Now;
        cardResultados.IsVisible = false;
    }
}