/**
 * @license Copyright (c) 2003-2014, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see LICENSE.md or http://ckeditor.com/license
 */

CKEDITOR.editorConfig = function( config ) {
	// Define changes to default configuration here. For example:
	 config.language = 'en';
    // config.uiColor = '#AADC6E';

	 config.filebrowserBrowseUrl = '/ckfinder/ckfinder.html';
	 config.filebrowserImageBrowseUrl = '/ckfinder/ckfinder.html?type=Images';
	 config.filebrowserFlashBrowseUrl = '/ckfinder/ckfinder.html?type=Flash';
	 config.filebrowserUploadUrl = '/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Files';
	 config.filebrowserImageUploadUrl = '/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Images';
	 config.filebrowserFlashUploadUrl = '/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Flash';

	 config.toolbar =[
	 	{ name: 'document', items: [ 'Source', '-', 'NewPage', 'Preview'] },
	 	{ name: 'clipboard', items: [ 'Cut', 'Copy', 'Paste', '-', 'Undo', 'Redo' ] },
	 	{ name: 'linkitems', items: [ 'Link', 'Unlink', 'Anchor'] },
	 	{ name: 'mediaitems', items: [ 'Image', 'Table', 'HorizontalRule', 'SpecialChar', 'Iframe'] },
    	
    	{ name: 'basicstyles', items: [ 'Bold', 'Italic', 'Underline', 'Strike', 'Subscript', 'Superscript'] },
    	{ name: 'textalign', items: [ 'JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock'] },
    	{ name: 'list', items: [ 'NumberedList', 'BulletedList', 'Outdent', 'Indent', 'Blockquote'] },
    	'/',
    	{ name: 'styles', items: [ 'Format', 'Font', 'FontSize', 'TextColor', 'BGColor'] },
    	{ name: 'maximize', items: [ 'Maximize'] }
	 ];
};
