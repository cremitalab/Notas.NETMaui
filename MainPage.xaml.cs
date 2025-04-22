namespace NotasDbanda
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
            pkEstudiantes.Items.Add("Mario");
            pkEstudiantes.Items.Add("Rocio");
            pkEstudiantes.Items.Add("Pablo");
            pkEstudiantes.Items.Add("Marlene");
        }

        public void OnClickCalculoNotas(object sender, EventArgs e)
        {
            string errores = "";

            // Validar campos vacíos
            if (pkEstudiantes.SelectedItem == null)
                errores += "Seleccione un estudiante.\n";
      
            if (string.IsNullOrWhiteSpace(entrySeguimiento1.Text))
                errores += "Ingrese la Nota de Seguimiento 1.\n";

            if (string.IsNullOrWhiteSpace(entryExamen1.Text))
                errores += "Ingrese la Nota del Examen 1.\n";

            if (string.IsNullOrWhiteSpace(entrySeguimiento2.Text))
                errores += "Ingrese la Nota de Seguimiento 2.\n";

            if (string.IsNullOrWhiteSpace(entryExamen2.Text))
                errores += "Ingrese la Nota del Examen 2.\n";

            // Validar números y rangos
            decimal seg1, ex1, seg2, ex2;

            if (!decimal.TryParse(entrySeguimiento1.Text, out seg1))
                errores += "La Nota de Seguimiento 1 debe ser un número válido.\n";
            else if (seg1 < 0.1m || seg1 > 10)
                errores += "La Nota de Seguimiento 1 debe estar entre 0.1 y 10.\n";

            if (!decimal.TryParse(entryExamen1.Text, out ex1))
                errores += "La Nota del Examen 1 debe ser un número válido.\n";
            else if (ex1 < 0.1m || ex1 > 10)
                errores += "La Nota del Examen 1 debe estar entre 0.1 y 10.\n";

            if (!decimal.TryParse(entrySeguimiento2.Text, out seg2))
                errores += "La Nota de Seguimiento 2 debe ser un número válido.\n";
            else if (seg2 < 0.1m || seg2 > 10)
                errores += "La Nota de Seguimiento 2 debe estar entre 0.1 y 10.\n";

            if (!decimal.TryParse(entryExamen2.Text, out ex2))
                errores += "La Nota del Examen 2 debe ser un número válido.\n";
            else if (ex2 < 0.1m || ex2 > 10)
                errores += "La Nota del Examen 2 debe estar entre 0.1 y 10.\n";

            // Mostrar errores si existen
            if (!string.IsNullOrEmpty(errores))
            {
                DisplayAlert("Errores encontrados", errores, "OK");
                return;
            }

            // Si todo está bien, calculamos
            decimal nota1 = (seg1 * 0.3m) + (ex1 * 0.2m);
            decimal nota2 = (seg2 * 0.3m) + (ex2 * 0.2m);

            lblNotaParcial1.Text = $"Nota Parcial 1: {nota1:F2}";
            lblNotaParcial2.Text = $"Nota Parcial 2: {nota2:F2}";

            // Guarda para usarlas en el segundo botón si quieres
            decimal notaParcial1 = nota1;
            decimal notaParcial2 = nota2;
        }
        public void OnclicVerificarPase(object sender, EventArgs e)
        {
            // Extraer valores numéricos desde los labels
            bool esParcial1Valido = decimal.TryParse(lblNotaParcial1.Text.Replace("Nota Parcial 1: ", ""), out decimal parcial1);
            bool esParcial2Valido = decimal.TryParse(lblNotaParcial2.Text.Replace("Nota Parcial 2: ", ""), out decimal parcial2);

            if (!esParcial1Valido || !esParcial2Valido)
            {
                DisplayAlert("Error", "Realice el cálculo de las notas primero.", "Cancelar");
                return;
            }

            decimal notaFinal = parcial1 + parcial2;
            string estado = "";

            if (notaFinal >= 7)
                estado = "Aprobado";
            else if (notaFinal >= 5)
                estado = "Complementario";
            else
                estado = "Reprobado";

            string mensaje = $"Estudiante: {pkEstudiantes.SelectedItem}\n" +
                             $"Fecha: {dpDate.Date:dd/MM/yyyy}\n" +
                             $"Nota Parcial 1: {parcial1:F2}\n" +
                             $"Nota Parcial 2: {parcial2:F2}\n" +
                             $"Nota Final: {notaFinal:F2}\n" +
                             $"Estado: {estado}";

            DisplayAlert("Resultado Final", mensaje, "OK");
        }

    }

} 

