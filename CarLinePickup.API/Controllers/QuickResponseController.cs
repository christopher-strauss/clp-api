//using System;
//using AutoMapper;
//using CarLinePickup.API.Models.Response;
//using CarLinePickup.Domain.Services.Interfaces;
//using QRCoder;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using System.Drawing;

//namespace CarLinePickup.API.Controllers
//{
//    [ApiVersion("1.0")]
//    [Produces("application/json")]
//    [Route("api/v{v:apiVersion}/qrcontroller")]
//    public class QuickResponseController : Controller
//    {
//        public QuickResponseController()
//        {

//        }

//        [HttpGet("ping")]
//        public IActionResult Ping()
//        {
//            return Ok();
//        }

//        [HttpGet("{encryptionstring}")]
//        public async Task<IActionResult> Get(string encryptionString)
//        {
//            //QRCoder****************************
//            //https://github.com/codebude/QRCoder

//            QRCodeGenerator qrGenerator = new QRCodeGenerator();
//            QRCodeData qrCodeData = qrGenerator.CreateQrCode(encryptionString, QRCodeGenerator.ECCLevel.Q);
//            QRCode qrCode = new QRCode(qrCodeData);
//            Bitmap qrCodeImage = qrCode.GetGraphic(20);

//            var response = qrCodeImage;

//            return Ok(response);
//        }
//    }
//}