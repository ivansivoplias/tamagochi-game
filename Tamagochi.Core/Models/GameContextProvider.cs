﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tamagochi.Infrastructure.Abstract;

namespace Tamagochi.Core.Models
{
    public class GameContextProvider : IGameContextProvider
    {
        public IGameContext GetGameContext(ISerializer serializer, ISettings settings)
        {
            IGameContext context;
            try
            {
                serializer.Initialize(settings.GameContextFilename);
                context = serializer.Deserialize<GameContext>();
            }
            catch
            {
                context = GameContext.GetDefault(serializer);
            }
            return context;
        }
    }
}