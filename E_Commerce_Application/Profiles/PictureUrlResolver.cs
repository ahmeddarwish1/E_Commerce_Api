using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Execution;
using E_Commerce_Application.Dtos.Products;
using E_Commerce_Domain.Entities.Products;
using Microsoft.Extensions.Options;

namespace E_Commerce_Application.Profiles
{
    public class PictureUrlResolver : IValueResolver<Product, ProductDto, string>
    {
        private readonly UrlSetting _urlSettings;
        public PictureUrlResolver(IOptions<UrlSetting> options)
        {
            _urlSettings = options.Value; 
        }

         
        public string Resolve(Product source, ProductDto destination, string destMember, ResolutionContext context)
        {
            var baseurl = _urlSettings.BaseUrl.TrimEnd('/');
            var path = source.PictureUrl.TrimStart('/');
            return $"{baseurl}/Files/{path}";
        }
    }
    public class UrlSetting
    {
        public string BaseUrl { get; set; }
    }
}
