﻿using Microsoft.EntityFrameworkCore;

namespace CunaBackendCodingChallenge.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
    }
}
