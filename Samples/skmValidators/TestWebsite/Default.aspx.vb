
Partial Class _Default
    Inherits System.Web.UI.Page

    Protected Sub SubmitFirstDemo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles SubmitFirstDemo.Click
        'If the page is valid, send user to Done.htm
        If Page.IsValid() Then
            Response.Redirect("Done.htm")
        End If
    End Sub

    Protected Sub SubmitSecondDemo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles SubmitSecondDemo.Click
        If Page.IsValid Then
            Response.Redirect("Done.htm")
        End If
    End Sub

    Protected Sub SubmitThirdDemo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles SubmitThirdDemo.Click
        If Page.IsValid Then
            Response.Redirect("Done.htm")
        End If
    End Sub

    Protected Sub SubmitFourthDemo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles SubmitFourthDemo.Click
        If Page.IsValid Then
            Response.Redirect("Done.htm")
        End If
    End Sub

End Class
