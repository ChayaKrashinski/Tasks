using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using todoList.Models;
using todoList.Interfaces;
using System;
using Microsoft.AspNetCore.Authorization;
using todoList.Services;
using todoList.Models;
using System.Security.Claims;

namespace todoList.Controllers;

[ApiController]
[Route("todo/admin")]
public class adminController : ControllerBase
{



// [Authorize(Policy = "Admin")]

}

