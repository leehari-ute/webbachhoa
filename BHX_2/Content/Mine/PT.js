(function(baseElement, pages, pageShow){
  var pageNum = 0, pageOffset = 0;

  function _initNav(){
    //create pages
    for(i=1;i<pages+1;i++){
      $((i==1?'<li class="active">':'<li>')+(i)+'</li>').appendTo('.nav-pages', baseElement).css('min-width',pages.toString().length+'em');
    }
    
    //calculate initial values
    function ow(e){return e.first().outerWidth()}
    var w = ow($('.nav-pages li', baseElement)),bw = ow($('.nav-btn', baseElement));
    baseElement.css('width',w*pageShow+(bw*2)+'px');
    $('.nav-pages', baseElement).css('margin-left',bw+'px');
    
    //init events
    baseElement.on('click', '.nav-pages li, .nav-btn', function(e){
      if($(e.target).is('.nav-btn')){
        var toPage = $(this).hasClass('prev') ? pageNum-1 : pageNum+1;
      }else{
        var toPage = $(this).index(); 
      }
      _navPage(toPage);
    });
  }

  function _navPage(toPage){
    var sel = $('.nav-pages li', baseElement), w = sel.first().outerWidth(),
        diff = toPage-pageNum;
    
    if(toPage>=0 && toPage <= pages-1){
      sel.removeClass('active').eq(toPage).addClass('active');
      pageNum = toPage;
    }else{
      return false;
    }
    
    if(toPage<=(pages-(pageShow+(diff>0?0:1))) && toPage>=0){
      pageOffset = pageOffset + -w*diff;  
    }else{
      pageOffset = (toPage>0)?-w*(pages-pageShow):0;
    }
    
    sel.parent().css('left',pageOffset+'px');
  }
  
  _initNav();

})($('.pagination'), 25, 5);