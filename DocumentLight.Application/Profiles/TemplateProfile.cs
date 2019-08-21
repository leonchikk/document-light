using AutoMapper;
using DocumentLight.Application.Models.Responses;
using DocumentLight.Application.Settings;
using DocumentLight.Core.Entities;
using DocumentLight.System.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentLight.Application.Profiles
{
    public class TemplateProfile: Profile
    {
        public TemplateProfile()
        {
            CreateMap<Template, TemplateResponse>()
                .ForMember(m => m.TemplateId, opt => opt.MapFrom(x => x.Id))
                .ForMember(m => m.Link, opt => opt.MapFrom(x => $"api/files/{x.File.FileName}"))
                .ForMember(m => m.FileId, opt => opt.MapFrom(x => x.File.Id));
        }
    }
}
