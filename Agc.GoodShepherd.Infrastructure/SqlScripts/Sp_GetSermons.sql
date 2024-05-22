create or alter proc Sp_GetSermons @pageindex int = 1,
                                   @pagesize int = 10,
                                   @startDate datetime,
                                   @endDate datetime,
                                   @orderBy nvarChar(50) = null,
                                   @keyword nvarChar(Max) = null,
                                   @sermonId nvarChar(50) = null,
                                   @totalCount int OUT
as
begin
    select s.*, sm.Url as ImageUrl
    from Sermons s
             left join dbo.SermonMedia sm on s.Id = sm.SermonId

    where CAST(s.DateCreated as date) between CAST(@startDate AS date) and CAST(@endDate as date)
      and (
        @keyword = '' or
        s.BibleBook like '%' + @keyword + '%' or
        s.BibleVerse like '%' + @keyword + '%' or
        s.BibleChapter like '%' + @keyword + '%' or
        s.Title like '%' + @keyword + '%' or
        s.Body like '%' + @keyword + '%' or
        s.Minister like '%' + @keyword + '%'
        )
    and (@sermonId='' or s.Id = @sermonId)

    order by case when @orderBy = 'DESC' then s.DateCreated end desc,
             case when @orderBy = 'ASC' then s.DateCreated end


    offset (@pagesize * (@pageIndex - 1)) rows fetch next @pagesize rows only

    select @totalCount = count(s.Id)
    from Sermons s
             left join dbo.SermonMedia sm on s.Id = sm.SermonId

    where CAST(s.DateCreated as date) between CAST(@startDate AS date) and CAST(@endDate as date)
      and (
        @keyword = '' or
        s.BibleBook like '%' + @keyword + '%' or
        s.BibleVerse like '%' + @keyword + '%' or
        s.BibleChapter like '%' + @keyword + '%' or
        s.Title like '%' + @keyword + '%' or
        s.Body like '%' + @keyword + '%' or
        s.Minister like '%' + @keyword + '%'
        )
      and (@sermonId='' or s.Id = @sermonId)

end