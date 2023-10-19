using CarLinePickup.Domain.Exceptions;
using CarLinePickup.Domain.Exceptions.ErrorCodes;

namespace CarLinePickup.Domain.Services
{
    public class ExceptionService
    {
        public ExceptionService()
        {
        }

        public TileException ParseTileException(TileException tileException)
        {
            switch ((int)tileException.HttpStatus)
            {
                case 409:
                    tileException.ErrorCode = TileCodes.UserNotFound;
                    break;
            }

            return tileException;
        }
    }
}
