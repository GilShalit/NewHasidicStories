# Hasidic Stories Browser
This is the code for the Blazor WASM client that allows close and distant reading in a TEI-XML repository hosted on an eXist-db server.
## eXist-db Dependencies
The client makes use of custom endpoints developped on the eXist-db that aggregate information and send it out as an XML field in a JSON stream. Examples include ``api/get-storyinfo`` called in ``Index.razor.cs``.
## TEI-Publisher Dependencies
The client integrates TEI-Publisher HTML5 components to display text fragments that has been processed by an ODD. The secret sauce comes from including the following lines in ``index.html``:
```
<script src="https://unpkg.com/@webcomponents/webcomponentsjs@2.4.3/webcomponents-loader.js"></script>
<script type="module" src="https://unpkg.com/@teipublisher/pb-components@latest/dist/pb-components-bundle.js"></script>
```
This allows building a pgae, very similar to a TEI-Publisher template, where a ``<pb-page`` contains ``<pb-document>``, ``<pb-view>`` etc. - see ``Edition.razor``. Another neat possibility is the Blazor component model, so a component such as ``StoryTEI.razor`` which includes a TEI-Publisher template in a ``<pb-document>`` HTML5 component, can be included in other components or pages. 
