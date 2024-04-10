
using api.TransferModels;
using infrastructure.datamodels;
using Microsoft.AspNetCore.Mvc;
using service.services;

namespace api.controller;

[ApiController]
public class CurrencyController : Controller
{
    private readonly CurrencyService _currencyService;

    public CurrencyController(CurrencyService currencyService)
    {
        _currencyService = currencyService;
    }

    
    //Makes a ResponseDTO which contains a success message and the data from the service layer.
    [HttpGet]
    [Route("/currency/get")]
    public ResponseDto GetCurrencyHistory()
    {
        return new ResponseDto()
        {
            MessageToClient = "Successfully got all prior conversions of currency",
            ResponseData = _currencyService.GetCurrencyHistory()
        };
    }

    //Makes a ResponseDto which contains a success message and the new data from the service layer.
    [HttpPost]
    [Route("/currency/post")]
    public ResponseDto PostCurrency([FromBody] CurrencyModel currencyModel)
    {
        return new ResponseDto()
        {
            MessageToClient = "Successfully created new entry of currency conversion",
            ResponseData = _currencyService.PostCurrency(currencyModel)
        };
    }
}