namespace Xtramile.Weather.MainApp.ServiceContract.Response
{
    public class GenericResponse<T> : BaseResponse
    {
        public T Data { get; set; }
    }
}
