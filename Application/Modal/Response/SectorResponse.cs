namespace Application.Modal.Response
{
    // DTO PARA MOSTRAR LOS DETALLES DEL SECTOR Y SU PRECIO
    public class SectorResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Capacity { get; set; }
    }
}