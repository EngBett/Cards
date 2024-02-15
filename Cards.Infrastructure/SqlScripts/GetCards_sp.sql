create or alter proc Sp_GetTransactions @pageindex int = 1,
                                        @pagesize int = 10,
                                        @startDate datetime,
                                        @endDate datetime,
                                        @cardStatus nvarChar(50) = null,
                                        @sortBy nvarChar(50) = null,
                                        @orderBy nvarChar(50) = null,
                                        @keyword nvarChar(Max) = null,
                                        @applicationUserId nvarChar(50)=null,
                                        @totalCount int OUT
    
as
begin
    select c.*
    from Cards c

    where CAST(c.DateCreated as date) between CAST(@startDate AS date) and CAST(@endDate as date)
      and (@keyword = '' or c.Name like '%' + @keyword + '%' or
           c.Color like '%' + @keyword + '%' or 
           c.Description like '%' + @keyword + '%' or
           c.Status like '%' + @keyword + '%')
      
      and (@cardStatus = '' or @cardStatus = c.Status)
      and (@applicationUserId = '' or @applicationUserId = c.ApplicationUserId)

    order by case when @sortBy = 'DateCreated' and @orderBy = 'DESC' then c.DateCreated end desc,
             case when @sortBy = 'DateCreated' and @orderBy = 'ASC' then c.DateCreated end,
             case when @sortBy = 'Color' and @orderBy = 'DESC' then c.Color end desc,
             case when @sortBy = 'Color' and @orderBy = 'ASC' then c.Color end,
             case when @sortBy = 'Status' and @orderBy = 'DESC' then c.Status end desc,
             case when @sortBy = 'Status' and @orderBy = 'ASC' then c.Status end,
             case when @sortBy = 'Name' and @orderBy = 'DESC' then c.Name end desc,
             case when @sortBy = 'Name' and @orderBy = 'ASC' then c.Name end
    offset (@pagesize * (@pageIndex - 1)) rows fetch next @pagesize rows only


    select @totalCount = count(c.Id)
    from Cards c

    where CAST(c.DateCreated as date) between CAST(@startDate AS date) and CAST(@endDate as date)
      and (@keyword = '' or c.Name like '%' + @keyword + '%' or
           c.Color like '%' + @keyword + '%' or
           c.Description like '%' + @keyword + '%' or
           c.Status like '%' + @keyword + '%')

      and (@cardStatus = '' or @cardStatus = c.Status)
      and (@applicationUserId = '' or @applicationUserId = c.ApplicationUserId)

end