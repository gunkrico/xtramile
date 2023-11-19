using System.Collections.Generic;

namespace Xtramile.Weather.MainApp.ServiceContract.Response
{
    public class GenericGetDtoCollectionResponse<TDto> : BaseResponse
    {
        #region Fields

        private List<TDto> dtoList;

        #endregion

        #region Constructor

        public GenericGetDtoCollectionResponse()
        {
        }

        public GenericGetDtoCollectionResponse(int capacity)
        {
            this.dtoList = new List<TDto>(capacity);
        }

        #endregion

        #region Properties

        public ICollection<TDto> DtoCollection
        {
            get { return this.dtoList ?? (this.dtoList = new List<TDto>()); }
        }

        #endregion
    }
}
