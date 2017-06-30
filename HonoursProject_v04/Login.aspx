<%@ Page Language="C#" Async="true" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="HonoursProject_v04.Login" %>

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
					<h1>Login to Your Account</h1><br/>
				    <input runat="server" id="txtUsername" type="text" name="user" placeholder="Username"/>
					<input runat="server" id="txtPwd" type="password" name="pass" placeholder="Password"/>

                    <asp:Button runat="server" Text="Log In" ID="btnLogin" OnClick="btnLogin_Click" CssClass="btn btn-primary" />
					<label runat="server" id="lblError" style="color:red; font:bold" ></label>
				  <div class="login-help">
					<a href="ForgotPassword.aspx">Forgot Password</a>
				  </div>
				</div>
			</div>
		  </div>       
    </form>
</body>
</html>
