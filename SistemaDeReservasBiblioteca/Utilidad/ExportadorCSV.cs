using SistemaDeReservasBiblioteca.Modelos;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SistemaDeReservasBiblioteca.Utilidades
{
    public static class ExportadorCSV
    {
        public static void ExportarLibros(List<Libro> libros, string rutaArchivo)
        {
            StringBuilder csv = new StringBuilder();
            csv.AppendLine("ISBN,Titulo,Autor,Editorial,AnioPublicacion,Genero,NumeroCopias");
            foreach (var libro in libros)
            {
                csv.AppendLine($"{libro.ISBN},{libro.Titulo},{libro.Autor},{libro.Editorial},{libro.AnioPublicacion},{libro.Genero},{libro.NumeroCopias}");
            }
            File.WriteAllText(rutaArchivo, csv.ToString());
        }
    }
}
