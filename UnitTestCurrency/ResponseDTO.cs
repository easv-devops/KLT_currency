﻿namespace Test;

public class ResponseDto<T>
{
    public string MessageToClient { get; set; }
    public T? ResponseData { get; set; }
}