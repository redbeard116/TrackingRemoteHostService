﻿using System.Threading.Tasks;
using TrackingRemoteHostService.Models;

namespace TrackingRemoteHostService.Services.IAuthService
{
    public interface IAuthService
    {
        /// <summary>
        /// Авторизация пользователя
        /// </summary>
        /// <param name="userAuth">Авторизационные данные</param>
        /// <returns></returns>
        Task<Responce> AuthUser(UserAuth userAuth);
    }
}
