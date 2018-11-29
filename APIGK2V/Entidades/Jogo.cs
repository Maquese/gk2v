namespace APIGK2V.Entidades
{
    public class Jogo : EntidadeBase
    {
        public string PrimeiraSelecao { get; set; }
        public string SegundaSelecao { get; set; }
        public string Vencedor { get; set; }
        public int Fase { get; set; }
    }

}