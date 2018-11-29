namespace APIGK2V.Entidades
{
    public class Aposta : EntidadeBase
    {
        public string CodigoTemporada { get; set; }
        public int Pontos { get; set; }
        public Jogo Jogo { get; set; }
    }
}