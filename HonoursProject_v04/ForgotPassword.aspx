<%@ Page Language="C#" Async="true" AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs" Inherits="HonoursProject_v04.ForgotPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/bootstrap.css" rel="stylesheet" />
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="css/login.css" rel="stylesheet" />
</head>
<body>
    
    <form runat="server" style="text-align:center" >

        <div>
    	  <div class="modal-dialog">
				<div class="loginmodal-container">
					<h1>Reset Your Password</h1><br/>
				    <input runat="server" id="txtUsername" type="text" name="user" placeholder="Username"/>

                    <asp:Button runat="server" Text="Reset Password" ID="btnReset" CssClass="btn btn-primary" />
					<label runat="server" id="lblError" style="color:red; font:bold" ></label>
				    <div class="login-help">
                        <label runat="server" id="Label1">New password will be emailed to you.</label>
				    </div>
                    <div class="login-help">
					    <a href="Login.aspx">Login</a>
				    </div>
				</div>
			</div>
		  </div>
        
    </form>
</body>
</html>
