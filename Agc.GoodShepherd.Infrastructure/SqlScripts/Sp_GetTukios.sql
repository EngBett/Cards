create or alter proc Sp_GetTukios @pageindex int = 1,
                                        @pagesize int = 10,
                                        @startDate datetime,
                                        @endDate datetime,
                                        @orderBy nvarChar(50) = null,
                                        @keyword nvarChar(Max) = null,
                                        @totalCount int OUT
    
as
begin
select t.*,tm.Url as ImageUrl
from Tukios t left join dbo.TukioMedia tm on t.Id = tm.TukioId

where CAST(t.DateCreated as date) between CAST(@startDate AS date) and CAST(@endDate as date)
  and (
            @keyword = '' or
            t.Title like '%' + @keyword + '%' or
            t.SubTitle like '%' + @keyword + '%' or
            t.Body like '%' + @keyword + '%' or
            t.Id like '%' + @keyword + '%'
    )

order by case when @orderBy = 'DESC' then t.DateCreated end desc,
         case when @orderBy = 'ASC' then t.DateCreated end


offset (@pagesize * (@pageIndex - 1)) rows fetch next @pagesize rows only

select @totalCount = count(t.Id)
from Tukios t left join dbo.TukioMedia tm on t.Id = tm.TukioId

where CAST(t.DateCreated as date) between CAST(@startDate AS date) and CAST(@endDate as date)
  and (
            @keyword = '' or
            t.Title like '%' + @keyword + '%' or
            t.SubTitle like '%' + @keyword + '%' or
            t.Body like '%' + @keyword + '%' or
            t.Id like '%' + @keyword + '%'
    )

end