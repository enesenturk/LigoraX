namespace Base.Dto.BaseResponse
{
	public class BaseResponseDto
	{

		public string Message { get; set; } = string.Empty;
		public string Type { get; set; } = BaseResponseConstants.TypeSuccess;
		public object ResponseModel { get; set; } = null;
		public string ErrorCode { get; set; } = BaseResponseConstants.DefaultErrorCode;

	}
}
