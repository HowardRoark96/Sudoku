export default function requireAll(contextRequire) {
    contextRequire.keys().forEach(contextRequire);
}