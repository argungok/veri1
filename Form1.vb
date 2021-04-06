Imports System.Data
Imports System.Data.SqlClient
Public Class Form1
    Dim cnn As New SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Casper\Desktop\dersler\btep210\uygulamalar\veri1\Contacts.mdf"";Integrated Security=True;Connect Timeout=30")
    Dim cmd, cmd1 As New SqlCommand
    Dim ds As New DataSet
    Dim da As New SqlDataAdapter
    Dim rdr As SqlDataReader

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If cnn.State = ConnectionState.Closed Then
            cnn.Open()
            cmd.Connection = cnn
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Clear()
            cmd.CommandText = "select Name,Fax from Publishers where PubID=@PubID"
            cmd.Parameters.Add("@PubID", SqlDbType.Int).Value = ComboBox1.Text
            rdr = cmd.ExecuteReader
            If rdr.HasRows Then
                While rdr.Read
                    Label5.Text = rdr("Name")
                    Label7.Text = rdr("Fax")
                End While
            End If
        End If
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'TODO: Bu kod satırı 'ContactsDataSet.Publishers' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
        Me.PublishersTableAdapter.Fill(Me.ContactsDataSet.Publishers)


        If cnn.State = ConnectionState.Closed Then
            cnn.Open()

            cmd.Connection = cnn
            cmd.CommandText = CommandType.Text
            cmd.CommandText = "select PubID,Name,Fax from Publishers"
            da.SelectCommand = cmd
            da.Fill(ds)
            DataGridView2.DataSource = ds.Tables(0)

            cmd1.Connection = cnn
            cmd1.CommandText = "select PubID from Publishers"

            rdr = cmd1.ExecuteReader()
            If rdr.HasRows Then
                While rdr.Read
                    ComboBox1.Items.Add(rdr("PubID"))
                End While
            End If



            cnn.Close()

        End If
    End Sub
End Class
