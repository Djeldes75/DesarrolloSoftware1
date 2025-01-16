namespace SistemaDeReservasBiblioteca.Modelos
{
    public class Libro
    {
        public string ISBN { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public string Editorial { get; set; }
        public int AnioPublicacion { get; set; }
        public string Genero { get; set; }
        public int NumeroCopias { get; set; }
    }
}
