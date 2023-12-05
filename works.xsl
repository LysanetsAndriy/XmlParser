<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
<xsl:output method="html" encoding="UTF-8"/>
<xsl:template match="/">
  <html>
  <body>
  <h2>Список матеріалів</h2>
    <table border="1">
      <tr bgcolor="#fff">
        <th>Автор</th>
        <th>Назва матеріалу</th>
        <th>Факультет</th>
        <th>Кафедра</th>
        <th>Вид матеріалу</th>
        <th>Рік створення</th>
      </tr>
      <xsl:for-each select="//Work">
      <tr>
        <td><xsl:value-of select="Author"/></td>
        <td><xsl:value-of select="WorkName"/></td>
        <td><xsl:value-of select="Faculty"/></td>
        <td><xsl:value-of select="Department"/></td>
        <td><xsl:value-of select="WorkType"/></td>
        <td><xsl:value-of select="Year"/></td>
      </tr>
      </xsl:for-each>
    </table>
  </body>
  </html>
</xsl:template>
</xsl:stylesheet>