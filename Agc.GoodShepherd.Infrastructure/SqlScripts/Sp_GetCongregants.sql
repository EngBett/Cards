create or alter proc Sp_GetCongregants @pageindex int = 1,
                                        @pagesize int = 10,
                                        @startDate datetime,
                                        @endDate datetime,
                                        @orderBy nvarChar(50) = null,
                                        @keyword nvarChar(Max) = null,
                                        @totalCount int OUT
    
as
begin
    select c.*
    from Congregants c

    where CAST(c.DateCreated as date) between CAST(@startDate AS date) and CAST(@endDate as date)
      and (
            @keyword = '' or c.FirstName like '%' + @keyword + '%' or
            c.LastName like '%' + @keyword + '%' or
            c.Email like '%' + @keyword + '%' or
            c.MiddleName like '%' + @keyword + '%' or
            c.PhoneNumber like '%' + @keyword + '%'
          )

    order by case when @orderBy = 'DESC' then c.DateCreated end desc,
             case when @orderBy = 'ASC' then c.DateCreated end
    
    
    offset (@pagesize * (@pageIndex - 1)) rows fetch next @pagesize rows only

    select @totalCount = count(c.Id)
    from Congregants c

    where CAST(c.DateCreated as date) between CAST(@startDate AS date) and CAST(@endDate as date)
      and (
                @keyword = '' or c.FirstName like '%' + @keyword + '%' or
                c.LastName like '%' + @keyword + '%' or
                c.Email like '%' + @keyword + '%' or
                c.MiddleName like '%' + @keyword + '%' or
                c.PhoneNumber like '%' + @keyword + '%'
        )

end